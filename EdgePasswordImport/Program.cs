// Copyright 2018 Slava Kolobaev and Contributors
// This file is a part of EdgePasswordImport and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/gmanny/EdgePasswordImport

using System;
using System.IO;
using Windows.Security.Credentials;
using CsvHelper;
using Microsoft.Win32;

namespace EdgePasswordImport
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            PasswordVault pv = new PasswordVault();

            // retrieve file location
            string fileLocation;
            if (args.Length > 0)
            {
                fileLocation = args[0];
            }
            else
            {
                OpenFileDialog openDlg = new OpenFileDialog
                {
                    CheckFileExists = true,
                    DefaultExt = ".csv",
                    Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*",
                    FilterIndex = 0,
                    ShowReadOnly = true,
                    Title = "Select CSV with passwords"
                };

                if (openDlg.ShowDialog() != true)
                {
                    Console.Out.WriteLine("File open canceled.");
                    return;
                }

                fileLocation = openDlg.FileName;
            }

            using (StreamReader fileReader = new StreamReader(fileLocation))
            using (CsvReader csv = new CsvReader(fileReader))
            {
                csv.Configuration.RegisterClassMap<CsvRecordMap>();

                int count = 0, failed = 0;
                foreach (CsvRecord record in csv.GetRecords<CsvRecord>())
                {
                    try {
                        // get a URL without path
                        Uri uri = new Uri(record.url);
                        string url = $"{uri.Scheme}{Uri.SchemeDelimiter}{uri.Authority}/"; // slash at the end is important

                        PasswordCredential cr = null;

                        // try to retrieve the credential if it already exists
                        try
                        {
                            cr = pv.Retrieve(url, record.username);
                        } catch {}

                        if (cr != null)
                        {
                            cr.Password = record.password;
                        }
                        else
                        {
                            cr = new PasswordCredential(url, record.username, record.password);
                        }

                        // mark the credential as usable by Edge
                        cr.Properties["applicationid"] = new Guid("4e3cb6d5-2556-4cd8-a48d-c755c737cba6");

                        // and to make it look genuine
                        cr.Properties["application"] = "Internet Explorer";

                        // add/update
                        pv.Add(cr);

                        count++;
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine($"Failed to import account for {record.url} with login {record.username} and password length {record.password.Length} with error:");
                        Console.WriteLine(e.ToString());

                        failed++;
                    }
                }

                Console.Out.WriteLine($"Imported {count} credentials.");
                if (failed > 0)
                {
                    Console.Out.WriteLine($"Failed to import {failed} credentials.");
                }
            }
        }
    }
}

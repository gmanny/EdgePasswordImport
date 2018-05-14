// Copyright 2018 Slava Kolobaev and Contributors
// This file is a part of EdgePasswordImport and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/gmanny/EdgePasswordImport

using CsvHelper.Configuration;

namespace EdgePasswordImport
{
    public class CsvRecord
    {
        public string url;
        public string username;
        public string password;
    }

    public sealed class CsvRecordMap : ClassMap<CsvRecord>
    {
        public CsvRecordMap()
        {
            //AutoMap();

            Map(x => x.url)     .Name(nameof(CsvRecord.url));
            Map(x => x.username).Name(nameof(CsvRecord.username));
            Map(x => x.password).Name(nameof(CsvRecord.password));
        }
    }
}
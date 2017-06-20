using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.Collections.Generic;

namespace Ledger
{
	class valid
	{

//tests for proper number entry, true if proper
        static public bool dectest(string tc)
        {
            bool test = true;
            foreach (char n in tc)
            {
                if (!( n != '1' || n != '2' || n != '3' || n != '4' || n != '5' || n != '6' || n != '7' || n != '8' || n != '9' || n != '0' || n != '.'))
                    test = false;
            }
            return test;
        }

//tests for admin, returns line number if admin and the character exists, 0 otherwise
        static public int admin(string userid, string charname, string ssid, string apname, UserCredential c) 
        {
            int test = 0;
            int count = 0;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = c,
                ApplicationName = apname,
            });

            string range = "Users!D:E";
            SpreadsheetsResource.ValuesResource.GetRequest r = 
                service.Spreadsheets.Values.Get(ssid, range);
            ValueRange response = r.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                count++;
                if (row[0].ToString() == userid)
                    test = count;
            }

            if (test != 0)
            {
                test = 0;
                count = 0;
                range = "Characters!A1:D";
                r =
                    service.Spreadsheets.Values.Get(ssid, range);
                response = r.Execute();
                values = response.Values;

                foreach (var row in values)
                {
                    count++;
                    if (row[2].ToString() == charname)
                        test = count;
                }
            }

            return test;
        }

//tests if a character is owned by command giver, returns the number of the line if true, 0 otherwise
        static public int charcheck(string userid, string charname, string ssid, string apname, UserCredential c)
        {
            int test = 0;
            int count = 0;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = c,
                ApplicationName = apname,
            });

            string range = "Characters!A1:D";
            SpreadsheetsResource.ValuesResource.GetRequest r =
                service.Spreadsheets.Values.Get(ssid, range);
            ValueRange response = r.Execute();
            IList<IList<Object>> values = response.Values;
           
            foreach (var row in values)
            {
                test++;
                if (row[2].ToString() == charname)
                {
                    if (row[1].ToString() == userid)
                        count = test;
                }
            }
            
            return count;
        }
    }
}

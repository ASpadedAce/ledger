using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;

namespace Ledger
{
    class account
    {

//writes to specified cells
        static public void write(string range, List<object> oblist, UserCredential gcred, string ApplicationName)
        {
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = gcred,
                ApplicationName = ApplicationName,
            });

            ValueRange valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>> { oblist };
            
            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, creds.ssid(), range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            UpdateValuesResponse result2 = update.Execute();
        }

//finds' money in a character's bank account, returns null if it does not exist
        static public string bank(string name, string uid, UserCredential gcred, string ApplicationName)
        {
            string money = null;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = gcred,
                ApplicationName = ApplicationName,
            });

            string range = "Characters!A1:D";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(creds.ssid(), range);

            //finds user entry and records value
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                if (row[2].ToString() == name)
                    money = row[3].ToString();
            }

            return money;
        }

//grabs user's xp, returns null if nothing is found
        static public string xp(string name, UserCredential gcred, string ApplicationName)
        {
            string ammount = null;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = gcred,
                ApplicationName = ApplicationName,
            });


            string range = "Characters!A1:F";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(creds.ssid(), range);

            //finds user entry and records value
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                if (row[2].ToString() == name)
                {
                    ammount = row[5].ToString();
                }
            }

            return ammount;
        }

//returns the user id of a person with a name reference, returns null if none is found or level is invalid
        static public string level(string name, UserCredential gcred, string ApplicationName)
        {
            string ammount = null;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = gcred,
                ApplicationName = ApplicationName,
            });

            string range = "Characters!A1:H";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(creds.ssid(), range);
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                if (row[2].ToString() == name)
                {
                    ammount = row[7].ToString();
                }
            }

            if (!(ammount != "1" || ammount != "2" || ammount != "3" || ammount != "4" || ammount != "5" || ammount != "6" || ammount != "7" || ammount != "8" || ammount != "9" || ammount != "1" || ammount != "10" || ammount != "11" || ammount != "12" || ammount != "13" || ammount != "14" || ammount != "15" || ammount != "16" || ammount != "17" || ammount != "18" || ammount != "19" || ammount != "20"))
                ammount = null;

            return ammount;
        }

        //returns the user id of a person with a name reference, returns null if none is found or level is invalid
        static public string next(string name, UserCredential gcred, string ApplicationName)
        {
            string ammount = null;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = gcred,
                ApplicationName = ApplicationName,
            });


            string range = "Characters!A1:G";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(creds.ssid(), range);
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                if (row[2].ToString() == name)
                {
                    ammount = row[6].ToString();
                }
            }

            return ammount;
        }

        //returns the user id of a person with a name reference, returns null if none is found
        static public string unametoid(string name, UserCredential c, string appname)
        {
            string name2 = null;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = c,
                ApplicationName = appname,
            });

            string range = "Users!A1:B";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(creds.ssid(), range);
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                if (row[1].ToString() == name)
                {
                    name2 = row[0].ToString();
                }
            }
            return name2;
        }

//turns a user's id to name, returns null if they have not registered
        static public string idtouname(string id, UserCredential gcred, string appname)
        {
            string name = null;

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = gcred,
                ApplicationName = appname,
            });


            string range = "Users!A1:D";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(creds.ssid(), range);


            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            foreach (var row in values)
            {
                if (row[0].ToString() == id)
                {
                    name = row[1].ToString();
                }
            }

            return name;
        }
    }
}

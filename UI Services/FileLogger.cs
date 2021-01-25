using SPC.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SPC.Helper.Extension;

namespace IndexingInvoices
{
    class FileLogger : SPC.Services.ILogService
    {
        string _filename = "";

        public FileLogger()
        {
            _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{DateTime.Now.ToDateTimeString()}_log.txt");

            if (!File.Exists(_filename))
            {
                File.Create(_filename);
            }

            Content = new StringBuilder();
        }

        void ILogService.Log(string Source, string LogType, string key, string pAction, string data)
        {
            try
            {
                Content.AppendLine($"{Source}:=> {LogType}.{key}.{pAction}. Data = {data}{Environment.NewLine}");

                if (Content.Length > 2000)
                {
                    Flush();
                }

                if (Source=="End")
                {
                    Flush();
                }
            }
            catch (Exception)
            {
            }
        }

        StringBuilder Content = null;

        public   void Flush()
        {
            try
            {
                // Win client
                var output = Content.ToString();
                if (string.IsNullOrEmpty(output))
                    return;
                                
                    try
                    {
                       File.AppendAllText(_filename, output);
                        Content.Length = 0;
                    }
                    catch (Exception ex)
                    {
                        if ((ex) is System.IO.IOException)
                        {
                        }
                        else
                            throw;
                    }
                
            }
            catch (Exception ex)
            {
            }
        }

       
       
    }
}

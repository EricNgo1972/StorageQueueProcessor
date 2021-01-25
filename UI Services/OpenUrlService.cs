using SPC;
using SPC.Helper;
using SPC.Helper.Extension;
using SPC.Services;
using SPC.Services.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class OpenUrlService: SPC.Services.UI.IRunURLService
{
    public void Open(string pURL, bool Wait = false)
    {
        Process_Start(pURL);
    }

    public void OpenDocument(string pURL, Dictionary<string, string> options)
    {
        if (System.IO.File.Exists(pURL))
        {

        }
        else if (System.IO.Directory.Exists(pURL))
            Process_Start(pURL);
        else
            SPC.ServicesContainer.Get<IAlert>().Alert(ResStrConst.DoesNotExists(pURL, "Open"));
    }

    public static void Process_Start(string filename)
    {
        filename = filename.RegExpReplace("^$phoebus", FileRepository.GetAppFolder());
        filename = filename.RegExpReplace("^$template", FileRepository.GetTemplateFolder());

        if (filename.StartsWith("http:") || filename.StartsWith("https:"))
            Process.Start(filename.HttpEncode());
        else if (filename.MatchesRegExp("xls$|xlsx$|xlt$"))
            // ConfirmDialog.Confirm(ResStr("File '{0}' has been created. Please open it from Excel"))
            Process.Start("Excel.exe", string.Format("\"{0}\"", filename));
        else if (filename.MatchesRegExp("xls$|xlsx$|xlt$"))
        {

            Process.Start(filename);
        }
        else if (filename.MatchesRegExp("pdf$"))
        {
            SPC.ServicesContainer.Get<IWaitingPanel>().Done();
            Process.Start(filename);

        }
        else if (filename.MatchesRegExp("doc$|docx$|rtf$"))
        {
            SPC.ServicesContainer.Get<IWaitingPanel>().Done();
            Process.Start(filename);

        }
        else
        {
            SPC.ServicesContainer.Get<IWaitingPanel>().Done();
            Process.Start(filename);
        }
    }

    Task IRunURLService.RunAsync(CmdArg Arg)
    {
        Open(Arg.Url);

        return Task.CompletedTask;
    }


}

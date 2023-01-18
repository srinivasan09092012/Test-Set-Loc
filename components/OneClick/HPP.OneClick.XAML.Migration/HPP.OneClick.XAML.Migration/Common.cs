using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration
{
  public class Common
  {
    public static TfsTeamProjectCollection AuthenticateSourceControl()
    {

      NetworkCredential networkCredential = new NetworkCredential("msrinivasan@dxc.com", "s6e67zyz5vwneb2tmaygmhcumzgqehc37aazkwosskkvxwdhkbgq");
      VssBasicCredential basicAuthCredential = new VssBasicCredential(networkCredential);
      VssCredentials tfsClientCredentials = new VssCredentials(basicAuthCredential);
      tfsClientCredentials.PromptType = CredentialPromptType.DoNotPrompt;
      TfsTeamProjectCollection tfsTeamProjectCollection = new TfsTeamProjectCollection(new Uri("https://payerportfolio.visualstudio.com/"), tfsClientCredentials);
      tfsTeamProjectCollection.EnsureAuthenticated();
      return tfsTeamProjectCollection;
    }

  }
}

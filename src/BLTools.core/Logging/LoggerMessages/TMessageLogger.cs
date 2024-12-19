using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using BLTools.Core.Diagnostic.Logging;

namespace BLTools.Core.Diagnostic;

public partial class TMessageLogger {




  public ILogger Logger { get; private set; }

  public TMessageLogger(ILogger logger) {
    Logger = logger;
  }

  public void Message(string message, [CallerMemberName] string caller = "") {
    Logger.Log($"{caller} ==> {message}");
  }

  public void Ok(string additionalInfo = "", [CallerMemberName] string caller = "") {
    if (additionalInfo == "") {
      Logger.Log($"{caller} ==> {MESSAGE_OK}");
    } else {
      Logger.Log($"{caller} ==> {MESSAGE_OK} : {additionalInfo}");
    }
  }

  public void Failed(Exception ex, [CallerMemberName] string caller = "") {
    Logger.Log($"{caller} ==> {MESSAGE_FAILED} : {ex.Message}");
  }

  public void Failed(string additionalInfo = "", [CallerMemberName] string caller = "") {
    if (additionalInfo == "") {
      Logger.Log($"{caller} ==> {MESSAGE_FAILED}");
    } else {
      Logger.Log($"{caller} ==> {MESSAGE_FAILED} : {additionalInfo}");
    }
  }

}

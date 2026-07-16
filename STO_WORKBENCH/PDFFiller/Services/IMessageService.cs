using System;
using System.Collections.Generic;
using System.Text;

namespace STO_WORKBENCH.PDFFiller.Services
{
    public interface IMessageService
    {
        void ShowError(string message);
        void ShowWarning(string message);
        void ShowSuccess(string message);
        DialogResult ShowYesNo(string message);
    }
}

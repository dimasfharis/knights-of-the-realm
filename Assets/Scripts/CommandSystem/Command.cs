using UnityEngine;
using System;

namespace CommandSystem
{
    public class Command
    {
        public string CommandBy { get; private set; }

        public Action ExecuteAction { get; private set; }

        #region Constructors

        public Command(string commandBy, Action methodToExecute)
        {
            CommandBy = commandBy;
            ExecuteAction = methodToExecute;
        }

        #endregion

        #region Execution

        public void Execute()
        {
            ExecuteAction?.Invoke();
        }

        #endregion
    }
}
using UnityEngine;
using System.Collections.Generic;

namespace CommandSystem
{
    public class CommandManager
    {
        private List<Command> commandHistory = new List<Command>();

        #region History Management

        public void AddCommand(Command command)
        {
            commandHistory.Add(command);
        }

        #endregion
    }
}
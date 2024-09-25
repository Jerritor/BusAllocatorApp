using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusAllocatorApp.Vars;

namespace BusAllocatorApp
{
    public class Settings
    {
        Vars v { get; set; }

        //The spreadsheet demand upload mode on app startup
        //true = Individual Demand Mode (default), false = Total Demand Mode (all)
        //default true
        //bool IsIndividualDemandModeOnStartup { get; set; } = true;

        public Settings(Vars v)
        {
            this.v = v;
        }

        /**
        public Settings(Vars v, bool StartupMode)
        {
            this.v = v;
            IsIndividualDemandModeOnStartup = StartupMode;
        }**/

        #region Toggle Demand Mode
        //Sets the other completion flag state to Uninitialized and clears data
        void ToggleDemandMode(bool toggle)
        {
            if (toggle)
            {
                v.IsDeptsAndDemandsCompleted = ToggleState(v.IsDeptsAndDemandsCompleted);
                v.IsTotalDemandsCompleted = ToggleState(v.IsTotalDemandsCompleted);
            }
        }

        private CompletionState ToggleState(CompletionState currentState)
        {
            switch (currentState)
            {
                case CompletionState.Uninitialized:
                    return CompletionState.Initialized;  // Turn 0 (Uninitialized) into 1 (Initialized)
                case CompletionState.Initialized:
                case CompletionState.Completed:
                    return CompletionState.Uninitialized;  // Turn 1 or 2 into 0 (Uninitialized)
                default:
                    return currentState;  // Return the current state if it's something unexpected
            }
        }

        void SetDemandModeToComplete()
        {
            if (v.IsDeptsAndDemandsCompleted == CompletionState.Initialized)
            {
                v.IsDeptsAndDemandsCompleted = CompletionState.Completed;
            }
            else if (v.IsTotalDemandsCompleted == CompletionState.Initialized)
            {
                v.IsTotalDemandsCompleted = CompletionState.Completed;
            }
        }

        //Sets the Current Demand Mode State to Incomplete
        private void SetDemandModeToIncomplete()
        {
            //Is in DeptsAndDemands mode
            if (v.IsDeptsAndDemandsCompleted == CompletionState.Completed)
            {
                v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
            }
            //Is in TotalDemands mode
            else if (v.IsTotalDemandsCompleted == CompletionState.Completed)
            {
                v.IsTotalDemandsCompleted = CompletionState.Initialized;
            }
        }

        public void ClearDemandData()
        {
            SetDemandModeToIncomplete();


            if (v.IsDeptsAndDemandsCompleted == CompletionState.Initialized)
            {
                v.ClearDeptsAndDemandsData();
            }
            else if (v.IsTotalDemandsCompleted == CompletionState.Initialized)
            {
                v.ClearTotalDemandsData();
            }

            v.ClearAllDemandsInDataGridView();
        }

        #endregion
    }
}

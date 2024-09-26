using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /**
        //Sets the other completion flag state to Uninitialized and clears data
        //if SelectDemandMode is 1 or 2, the user is selecting the specific demand mode to swap to.
        //0 = toggle, 1 = isDeptsAndDemands Mode, 2 = TotalDemands Mode
        void ToggleDemandMode(byte selectDemandMode = 0)
        {
            // Toggle between DeptsAndDemands and TotalDemands based on the selected mode
            switch (selectDemandMode)
            {
                case 1:  // Activate Department Demands Mode
                    if (v.IsDeptsAndDemandsCompleted == CompletionState.Uninitialized)
                    {
                        v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        v.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        ClearDemandData();
                    }
                    break;
                case 2:  // Activate Total Demands Mode
                    if (v.IsTotalDemandsCompleted == CompletionState.Uninitialized)
                    {
                        v.IsTotalDemandsCompleted = CompletionState.Initialized;
                        v.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;
                        ClearDemandData();
                    }
                    break;
                default:  // Default behavior: toggle both states
                    v.IsDeptsAndDemandsCompleted = ToggleState(v.IsDeptsAndDemandsCompleted);
                    v.IsTotalDemandsCompleted = ToggleState(v.IsTotalDemandsCompleted);
                    break;
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

        //Sets the Current Demand Mode State to Incomplete (Initialized means that is the active mode but data is incomplete)
        private void SetDemandModeToInitialized()
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
            if (v.IsDeptsAndDemandsCompleted == CompletionState.Initialized)
            {
                v.ClearDeptsAndDemandsData();
                SetDemandModeToInitialized();
                v.ClearAllDemandsInDataGridView();
            }
            else if (v.IsTotalDemandsCompleted == CompletionState.Initialized)
            {
                v.ClearTotalDemandsData();
                SetDemandModeToInitialized();
                v.ClearAllDemandsInDataGridView();
            }
            else
            {
                MessageBox.Show("Could not determine which demand mode is active, so data could not be cleared." +
                    " Please contact your administrator to resolve this issue.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }**/

        //
        //if SelectDemandMode is 1 or 2, the user is selecting the specific demand mode to swap to.
        //0 = toggle, 1 = isDeptsAndDemands Mode, 2 = TotalDemands Mode
        public void ToggleDemandMode(byte selectDemandMode = 0)
        {
            v.OutputDemandModeToDebugConsole();
            switch (selectDemandMode)
            {
                case 1:  // Activate Department Demands Mode
                    if (v.IsDeptsAndDemandsCompleted == CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Swapping to DeptsAndDemands mode
                        v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        v.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                    }
                    //If mode is already active, don't do anything
                    break;

                case 2:  // Activate Total Demands Mode
                    if (v.IsTotalDemandsCompleted == CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Swapping to TotalDemands mode
                        v.IsTotalDemandsCompleted = CompletionState.Initialized;
                        v.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;                    }
                    //If mode is already active, don't do anything
                    break;

                default:  // Default behavior: toggle between modes
                    if (v.IsDeptsAndDemandsCompleted != CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Currently in DeptsAndDemands mode, switch to TotalDemands mode
                        v.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;
                        v.IsTotalDemandsCompleted = CompletionState.Initialized;
                    }
                    else if (v.IsTotalDemandsCompleted != CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Currently in TotalDemands mode, switch to DeptsAndDemands mode
                        v.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                    }
                    else
                    {
                        v.ClearTotalDemandsData();
                        v.ClearDeptsAndDemandsData();

                        // If neither mode is active, default to DeptsAndDemands mode
                        v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        v.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                    }
                    break;
            }
            v.OutputDemandModeToDebugConsole();
        }

        public void ClearDemandData(CompletionState modeToInitialize)
        {
            if (modeToInitialize == v.IsDeptsAndDemandsCompleted)
            {
                v.ClearDeptsAndDemandsData();
                v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                v.ClearAllDemandsInDataGridView();
            }
            else if (modeToInitialize == v.IsTotalDemandsCompleted)
            {
                v.ClearTotalDemandsData();
                v.IsTotalDemandsCompleted = CompletionState.Initialized;
                v.ClearAllDemandsInDataGridView();
            }
            else
            {
                MessageBox.Show("Invalid mode specified for clearing demand data.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //Polymorphism (one-case recursive) function that clears the data of the active mode
        public void ClearDemandData()
        {
            //if active mode is individual dept mode
            if (v.IsDeptsAndDemandsCompleted != CompletionState.Uninitialized)
            {
                ClearDemandData(v.IsDeptsAndDemandsCompleted);
            }
            //if active mode is total demand mode
            else if (v.IsTotalDemandsCompleted != CompletionState.Uninitialized)
            {
                ClearDemandData(v.IsTotalDemandsCompleted);
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

        #endregion
    }
}

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

        #region Toggle Demand Mode
        //
        //if SelectDemandMode is 1 or 2, the user is selecting the specific demand mode to swap to.
        //0 = toggle, 1 = isDeptsAndDemands Mode, 2 = TotalDemands Mode
        // In Settings Class
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
                        // Re-initialize DemandData
                        v.UpdateDepartmentsWithRoutesAndTimeSets(false);
                    }
                    break;

                case 2:  // Activate Total Demands Mode
                    if (v.IsTotalDemandsCompleted == CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Swapping to TotalDemands mode
                        v.IsTotalDemandsCompleted = CompletionState.Initialized;
                        v.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;
                        // Re-initialize TotalDemands if necessary
                    }
                    break;

                default:  // Default behavior: toggle between modes
                    if (v.IsDeptsAndDemandsCompleted != CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Currently in DeptsAndDemands mode, switch to TotalDemands mode
                        v.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;
                        v.IsTotalDemandsCompleted = CompletionState.Initialized;
                        // Re-initialize TotalDemands if necessary
                    }
                    else if (v.IsTotalDemandsCompleted != CompletionState.Uninitialized)
                    {
                        ClearDemandData();
                        // Currently in TotalDemands mode, switch to DeptsAndDemands mode
                        v.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        // Re-initialize DemandData
                        v.UpdateDepartmentsWithRoutesAndTimeSets(false);
                    }
                    else
                    {
                        v.ClearTotalDemandsData();
                        v.ClearDeptsAndDemandsData();

                        // If neither mode is active, default to DeptsAndDemands mode
                        v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        v.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        // Re-initialize DemandData
                        v.UpdateDepartmentsWithRoutesAndTimeSets(false);
                    }
                    break;
            }
            v.OutputDemandModeToDebugConsole();
        }

        private void ClearDemandData(CompletionState modeToInitialize)
        {
            if (modeToInitialize == v.IsDeptsAndDemandsCompleted)
            {
                v.ClearDeptsAndDemandsData();
                v.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                v.ClearAllDemandsInDataGridView();

                //Debug
                v.OutputDemandModeToDebugConsole();
            }
            else if (modeToInitialize == v.IsTotalDemandsCompleted)
            {
                v.ClearTotalDemandsData();
                v.IsTotalDemandsCompleted = CompletionState.Initialized;
                v.ClearAllDemandsInDataGridView();

                //Debug
                v.OutputDemandModeToDebugConsole();
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


        public void SetDemandModeToComplete()
        {
            if (v.IsDeptsAndDemandsCompleted == CompletionState.Initialized)
            {
                v.IsDeptsAndDemandsCompleted = CompletionState.Completed;
            }
            else if (v.IsTotalDemandsCompleted == CompletionState.Initialized)
            {
                v.IsTotalDemandsCompleted = CompletionState.Completed;
            }
            v.OutputDemandModeToDebugConsole();
        }

        #endregion

        public void SetIncompleteAllocs(bool canIncompleteDemands, bool isDebug = false)
        {
            v.canAllocateWithIncompeleteDepts = canIncompleteDemands;
            //debug prints
            if (isDebug) Debug.WriteLine($"canAllocateIncomplete = '{v.canAllocateWithIncompeleteDepts}'");
        }
    }
}

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
        public Vars vars { get; set; }

        //The spreadsheet demand upload mode on app startup
        //true = Individual Demand Mode (default), false = Total Demand Mode (all)
        //default true
        //bool IsIndividualDemandModeOnStartup { get; set; } = true;
        public Settings(Vars v)
        {
            this.vars = v;
        }

        #region Demand Mode Handling
        //
        //if SelectDemandMode is 1 or 2, the user is selecting the specific demand mode to swap to.
        //0 = toggle, 1 = isDeptsAndDemands Mode, 2 = TotalDemands Mode
        // In Settings Class
        public void ToggleDemandMode(byte selectDemandMode = 0)
        {
            vars.OutputDemandModeToDebugConsole();
            switch (selectDemandMode)
            {
                case 1:  // Activate Department Demands Mode
                    if (vars.IsDeptsAndDemandsCompleted == CompletionState.Uninitialized)
                    {
                        vars.ClearDemandData();
                        // Swapping to DeptsAndDemands mode
                        vars.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        vars.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        // Re-initialize DemandData
                        vars.UpdateDepartmentsWithRoutesAndTimeSets(false);
                    }
                    break;

                case 2:  // Activate Total Demands Mode
                    if (vars.IsTotalDemandsCompleted == CompletionState.Uninitialized)
                    {
                        vars.ClearDemandData();
                        // Swapping to TotalDemands mode
                        vars.IsTotalDemandsCompleted = CompletionState.Initialized;
                        vars.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;
                        // Re-initialize TotalDemands if necessary
                    }
                    break;

                default:  // Default behavior: toggle between modes
                    if (vars.IsDeptsAndDemandsCompleted != CompletionState.Uninitialized)
                    {
                        vars.ClearDemandData();
                        // Currently in DeptsAndDemands mode, switch to TotalDemands mode
                        vars.IsDeptsAndDemandsCompleted = CompletionState.Uninitialized;
                        vars.IsTotalDemandsCompleted = CompletionState.Initialized;
                        // Re-initialize TotalDemands if necessary
                    }
                    else if (vars.IsTotalDemandsCompleted != CompletionState.Uninitialized)
                    {
                        vars.ClearDemandData();
                        // Currently in TotalDemands mode, switch to DeptsAndDemands mode
                        vars.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        vars.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        // Re-initialize DemandData
                        vars.UpdateDepartmentsWithRoutesAndTimeSets(false);
                    }
                    else
                    {
                        vars.ClearTotalDemandsData();
                        vars.ClearDeptsAndDemandsData();

                        // If neither mode is active, default to DeptsAndDemands mode
                        vars.IsDeptsAndDemandsCompleted = CompletionState.Initialized;
                        vars.IsTotalDemandsCompleted = CompletionState.Uninitialized;
                        // Re-initialize DemandData
                        vars.UpdateDepartmentsWithRoutesAndTimeSets(false);
                    }
                    break;
            }
            vars.OutputDemandModeToDebugConsole();
        }
        #endregion

        public void SetIncompleteAllocs(bool canIncompleteDemands, bool isDebug = false)
        {
            vars.canAllocateWithIncompeleteDepts = canIncompleteDemands;
            //debug prints
            if (isDebug) Debug.WriteLine($"canAllocateIncomplete = '{vars.canAllocateWithIncompeleteDepts}'");
        }
    }
}

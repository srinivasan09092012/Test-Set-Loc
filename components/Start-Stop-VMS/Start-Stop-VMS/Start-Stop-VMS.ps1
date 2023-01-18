   
 function global:ExecuteRunbook{ 
param([string] $action, [string] $environment, [bool] $validatesuccessflagforaction, [bool] $validatesuccessflagforenvironment)   

try
{ 

if($validatesuccessflagforaction -and $validatesuccessflagforenvironment)
{
    if($environment -eq "beta")
{

$vmlist = "mapsbetaweb01,mapsbetaapp01,mapsbetabt01,mapsbetacot01,mapsbetacot02,mapsbetadbs01,mapsbetadmc02,mapsbetadmc01"
$params = @{"AzureSubscriptionId"="37039e92-12e3-47a5-8a66-81a97ea1f4a8";"AzureVMList"="$vmlist";"Action"="$action"}
$job = Start-AzAutomationRunbook -ResourceGroupName "MAPS_Beta" –AutomationAccountName "mapshc-automation-beta-acc01" –Name "Stop-Start-AzureVM" -Parameters $params
}

if($environment -eq "stage2")
{

$vmlist = "mapsstg2app01,mapsstg2t1cot01,mapsstg2t1ad01"
$params = @{"AzureSubscriptionId"="37039e92-12e3-47a5-8a66-81a97ea1f4a8";"AzureVMList"="$vmlist";"Action"="$action"}
$job = Start-AzAutomationRunbook -ResourceGroupName "MAPS_STAGES" –AutomationAccountName "mapshc-automation-stages-acc01" –Name "Stop-Start-AzureVM" -Parameters $params
}

if($environment -eq "stage3")
{

$vmlist = "mapsstg3app01,mapsstg3t1cot01,mapsstg3t1ad01"
$params = @{"AzureSubscriptionId"="37039e92-12e3-47a5-8a66-81a97ea1f4a8";"AzureVMList"="$vmlist";"Action"="$action"}
$job = Start-AzAutomationRunbook -ResourceGroupName "MAPS_STAGES" –AutomationAccountName "mapshc-automation-stages-acc01" –Name "Stop-Start-AzureVM" -Parameters $params
}

$doLoop = $true
While ($doLoop) {
 if($environment -eq "beta")
 {
    $job = Get-AzAutomationJob -ResourceGroupName "MAPS_Beta" –AutomationAccountName "mapshc-automation-beta-acc01" -Id $job.JobId
  $status = $job.Status
  $doLoop = (($status -ne "Completed") -and ($status -ne "Failed") -and ($status -ne "Suspended") -and ($status -ne "Stopped"))    
  if($status -eq "Completed")
  {
       Get-AzAutomationJobOutput -ResourceGroupName "MAPS_Beta" –AutomationAccountName "mapshc-automation-beta-acc01" -Id $job.JobId –Stream Output  
  }
 }
  if($environment -eq "stage2")
 {
  $job = Get-AzAutomationJob -ResourceGroupName "MAPS_STAGES" –AutomationAccountName "mapshc-automation-stages-acc01" -Id $job.JobId
  $status = $job.Status
  $doLoop = (($status -ne "Completed") -and ($status -ne "Failed") -and ($status -ne "Suspended") -and ($status -ne "Stopped"))    
  if($status -eq "Completed")
  {
       Get-AzAutomationJobOutput -ResourceGroupName "MAPS_STAGES" –AutomationAccountName "mapshc-automation-stages-acc01" -Id $job.JobId –Stream Output  
  }
 }
 if($environment -eq "stage3")
 {
    $job = Get-AzAutomationJob -ResourceGroupName "MAPS_STAGES" –AutomationAccountName "mapshc-automation-stages-acc01" -Id $job.JobId
  $status = $job.Status
  $doLoop = (($status -ne "Completed") -and ($status -ne "Failed") -and ($status -ne "Suspended") -and ($status -ne "Stopped"))    
  if($status -eq "Completed")
  {
       Get-AzAutomationJobOutput -ResourceGroupName "MAPS_STAGES" –AutomationAccountName "mapshc-automation-stages-acc01" -Id $job.JobId –Stream Output  
  }
 }
}

if($status -eq "Completed")
  {
        Write-Host 'All activites got completed successfully'
  }
  else
  {
        Write-Host 'Error processing the request'
  }  
}
else
{
   Write-Host "Validation Error.Please try again"
}

}
catch
{
    Write-Output "Error processing request.Please try again" 
    Write-Output $_.Exception.Message
}
}

function global:validate{ 
param([string] $action, [string] $environment) 

[bool] $validatesuccessflagforenvironment = $true
[bool] $validatesuccessflagforaction = $true

  if(($environment -ne "beta") -and ($environment -ne "stage2") -and ($environment -ne "stage3") -or (($action -ne "start") -and ($action -ne "stop")))
  { 
        if(($environment -ne "beta") -and ($environment -ne "stage2") -and ($environment -ne "stage3"))
        {
	         Write-Host "Specify one of this Environment either beta or stage2 or stage3"            
             $quit = Read-Host "If you want to Quit press Yes or NO"
             $quitinlowercase = $quit.ToLower()
             if($quitinlowercase -eq "yes")
             {
                $validatesuccessflagforenvironment = $false     
                Write-Host "You have quitted successfully"                
             }
             else
             {                
                $nameofenvironment = Read-Host 'Specify the Environment Name(Beta, stage2, stage3)?'
                $nameofenvironmentinlowercase = $nameofenvironment.ToLower()                 
                if(($nameofenvironmentinlowercase -ne "beta") -and ($nameofenvironmentinlowercase -ne "stage2") -and ($nameofenvironmentinlowercase -ne "stage3"))
                {
                      $validatesuccessflagforenvironment = $false  
                      Write-Host "Exceeded maximum number of retries.Please try again."   
                }
                else
                {
                    $validatesuccessflagforenvironment = $true  
                }
              }
            }                                
            if(($actioninlowercase -ne "start") -and ($actioninlowercase -ne "stop"))
            {                        
	                    Write-Host "Specify one of this Action either Start or Stop"
                        $quit = Read-Host "If you want to Quit press Yes or NO"
                        $quitinlowercase = $quit.ToLower()
                        if($quitinlowercase -eq "yes")
                        {
                            $validatesuccessflagforaction = $false   
                            Write-Host "You have quitted successfully"    
                        } 
                        else
                        {                                
                        $action = Read-Host 'Specify the Action(Start, Stop)'
                        $actioninlowercase = $action.ToLower()
                
                        if(($actioninlowercase -ne "start") -and ($actioninlowercase -ne "stop"))
                        {
                            $validatesuccessflagforaction = $false   
                            Write-Host "Exceeded maximum number of retries.Please try again."   
                        }
                        else
                        {   
                            $validatesuccessflagforaction = $true                         
                            ExecuteRunbook -action $actioninlowercase -environment $nameofenvironmentinlowercase -validatesuccessflagforaction $validatesuccessflagforaction -validatesuccessflagforenvironment $validatesuccessflagforenvironment
                        }                
                    }                                                    
              }
        }  
  else
  {            
            ExecuteRunbook -action $action -environment $environment -validatesuccessflagforaction $validatesuccessflagforaction -validatesuccessflagforenvironment $validatesuccessflagforenvironment
  }            
}
    
$nameofenvironment = Read-Host 'Specify the Environment Name(Beta, stage2, stage3)?'
$nameofenvironmentinlowercase = $nameofenvironment.ToLower()
$action = Read-Host 'Specify the Action(Start, Stop)'
$actioninlowercase = $action.ToLower()
validate -action $actioninlowercase -environment $nameofenvironmentinlowercase

        
        

        
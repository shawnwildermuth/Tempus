
$projs = @("Customers", "TimeBilling", "Workers", "WorkTypes")

foreach ($proj in $projs) {
  Write-Output "Building DB for Tempus.$($proj)"
  n ef database drop -p "./Tempus.$($proj)" --force
  n ef database update -p "./Tempus.$($proj)"
}

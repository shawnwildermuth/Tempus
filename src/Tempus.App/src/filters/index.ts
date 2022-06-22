// TODO, add currency specifier
var formatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD'
});

export function toMoney(val: number) : string {
  return formatter.format(val); 
}
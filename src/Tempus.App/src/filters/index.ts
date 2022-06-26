import { LocationEntity } from "../models";

// TODO, add currency specifier
var formatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD'
});

export function toMoney(val: number) : string {
  return formatter.format(val); 
}

export function toAddressBlock(val: LocationEntity) : string {
  if (!val) return "";
  const items = new Array<string>();
  
  items.push(val.lineOne);
  if (val.lineTwo) items.push(val.lineTwo);
  if (val.lineThree) items.push(val.lineThree);
  items.push(`${val.city}, ${val.stateProvince}  ${val.postalCode}`);
  if (val.country) items.push(val.country);

  return items.join("<br/>");
}
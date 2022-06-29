import { IEntity } from '../models/index';

declare global {
  interface Array<T> {
    replaceEntities(collection: Array<T>) : void ;
    replaceEntityInArray(item: IEntity) : void;
    removeEntityFromArray(item: IEntity) : void;
  }
 }


Array.prototype.replaceEntities = function<T>(collection: Array<T>) {
  this.splice(0, this.length, ...collection);
}

Array.prototype.replaceEntityInArray = function(item: IEntity) {
  const index = this.findIndex(p => p.id === item.id);
  this.splice(index, 1, item);
}

Array.prototype.removeEntityFromArray = function(item: IEntity) {
  const index = this.findIndex(p => p.id === item.id);
  this.splice(index, 1);
}

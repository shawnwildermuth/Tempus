import { IEntity } from './../models/index';
export function clone<T extends Object>(src: T): T {
  return Object.assign({}, src) as T;
}

export function mapToIEntity(src: any, dest: any) {
  for (const key in src) {
    if (!src.hasOwnProperty(key) || !dest.hasOwnProperty(key)) {
      throw "Bad Mapping from type to type";
    }

    if (Array.isArray(src[key])) {
      // Replace with new array of items
      const collection: [] = dest[key];
      collection.splice(0, collection.length, ...(src[key] as []));
    } else {
      // Just copy the value
      dest[key] = src[key];
    }
  }
}
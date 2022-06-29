export function clone<T extends Object>(src: T): T {
  return Object.assign({}, src) as T;
}
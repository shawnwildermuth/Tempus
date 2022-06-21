export function replaceArray<T>(prop: Array<T>, collection: Array<T>) {
  prop.splice(0, prop.length, ...collection);
}
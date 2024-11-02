export const toUpperFirstSumbol = (arr: string[]): string[] =>
  arr
    .map((item) => item[0].toUpperCase() + item.slice(1))
    .map((item) => item.replace(/_/g, " "));

export const toLowerFirstSumbol = (arr: string[]): string[] =>
  arr
    .map((item) => item[0].toLowerCase() + item.slice(1))
    .map((item) => item.replace(/ /g, "_"));



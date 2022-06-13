import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "beautify" })
export class BeautifyStringPipe implements PipeTransform {
  transform(value: string) {
    if (!value) return;

    let beautifiedValue = "";

    for (let i = 0; i < value.length; i++) {
      const char = value[i];

      if (this.isFirstOrLastChar(i, value) && char === "@") continue;

      const charUpper = value[i].toUpperCase();
      if (char === charUpper) {
        beautifiedValue += ` ${value[i].toLowerCase()}`;
      } else {
        beautifiedValue += value[i];
      }
    }

    return beautifiedValue;
  }

  private isFirstOrLastChar(i: number, value: string) {
    return i === 0 || i === value.length - 1;
  }
}

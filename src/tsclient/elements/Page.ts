export default interface Page {
    backgroundColor?: string | null;
    margin?: number | null;
    marginUnit?: "Point" | "Meter" | "Centimeter" | "Millimeter" | "Inch" | "Feet" | "Mil" | null;
    defaultFontSize?: number | null;
    direction?: "LTR" | "RTL" | null;
    fontFamily?: string | null;
    pageSize:
        | "A0"
        | "A1"
        | "A2"
        | "A3"
        | "A4"
        | "A5"
        | "A6"
        | "A7"
        | "A8"
        | "A9"
        | "A10"
        | "B0"
        | "B1"
        | "B2"
        | "B3"
        | "B4"
        | "B5"
        | "B6"
        | "B7"
        | "B8"
        | "B9"
        | "B10"
        | "Env10"
        | "EnvC4"
        | "EnvDL"
        | "Executive"
        | "Legal"
        | "Letter"
        | "ARCH_A"
        | "ARCH_B"
        | "ARCH_C"
        | "ARCH_D"
        | "ARCH_E"
        | "ARCH_E1"
        | "ARCH_E2"
        | "ARCH_E3";
}

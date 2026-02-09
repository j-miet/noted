/**
 * Return empty string for null/invalid inputs, otherwise the passed string itself
 */
export function nameCheck(name: string | null): string {
  if (name == null) {
    return "";
  } else if (name.trim() == "" || name.length > 50) {
    return "";
  } else {
    return name;
  }
}

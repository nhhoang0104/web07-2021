import EnumDataType from "@/constants/EnumDataType.js";
export default class Validation {
  /*
    Validate các trường bắt buộc
  */

  static validate(type, value) {
    if (type === EnumDataType.EMAIL) {
      return Validation.validateEmail(value);
    }

    return Validation.validateCommon(value);
  }

  static validateCommon(value) {
    let isValidated = value !== "" && value !== null && value !== undefined;
    let message = isValidated ? "" : "Không được bỏ trông trường này";
    return { isValidated, message };
  }

  /*
      Validate email
  */
  static validateEmail(value) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    let isValidated = re.test(String(value).toLowerCase());
    let message = isValidated ? "" : "Email không đúng định đạng";
    return { isValidated, message };
  }
}

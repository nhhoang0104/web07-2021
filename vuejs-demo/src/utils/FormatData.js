import moment from "moment";
import EnumGenderName from "@/constants/EnumGenderName";
export default class FormatData {
  /*
    định dạng ten gioi tinh
  */

  static formatGender(id) {
    if (id === null) return "";

    let tmp = EnumGenderName.find((item) => item.id === id.toString());
    return tmp.label;
  }

  /*
    định dạng ngày tháng năm để render lên table
  */

  static formatDate(date) {
    if (!date) return "";

    return moment(new Date(date)).format("DD/MM/YYYY");
  }

  /*
    định dạng năm tháng ngáy để post api
  */

  static formatDateInput(date) {
    if (!date) return null;

    return moment(new Date(date)).format("YYYY-MM-DD");
  }

  /*
    định dạng tiền
  */

  static formatMoney(n, currency = "") {
    if (n === " " || n === null || n === undefined) return "";
    if (typeof n === "number") n = n.toFixed();
    return (
      n.replace(/./g, function(c, i, a) {
        return i > 0 && (a.length - i) % 3 === 0 ? "." + c : c;
      }) + currency
    );
  }
}

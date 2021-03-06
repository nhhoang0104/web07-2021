class Common {
  /*
    định dạng ngày tháng năm để render lên table
  */
  static formatDate(date) {
    if (!date) return "";

    let tmp = new Date(date);

    let day = `${tmp.getDate() > 9 ? "" : "0"}${tmp.getDate()}`;
    let month = `${tmp.getMonth() > 8 ? "" : "0"}${tmp.getMonth() + 1}`;

    return `${day}\/${month}\/${tmp.getFullYear()}`;
  }

  /*
    định dạng năm tháng ngáy để post api
  */

  static formatDateInput(date) {
    if (!date) return null;
    var tmp = new Date(date);

    var day = ("0" + tmp.getDate()).slice(-2);
    var month = ("0" + (tmp.getMonth() + 1)).slice(-2);

    return tmp.getFullYear() + "-" + month + "-" + day;
  }

  /*
    định dạng tiền
  */

  static formatMoney(n, currency = "") {
    if (n === " " || n === null || n === undefined) return "";
    if (typeof n === "number") n = n.toFixed();
    return (
      n.replace(/./g, function (c, i, a) {
        return i > 0 && (a.length - i) % 3 === 0 ? "." + c : c;
      }) + currency
    );
  }

  /*
      Validate email
  */

  static validateEmail(email) {
    const re =
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  /*
    Tìm kiếm
  */

  static search(textSearch, data) {
    let result = [];
    textSearch = textSearch.toLocaleLowerCase();

    console.log(textSearch);

    if (!textSearch) return data;

    data.forEach((item) => {
      if (item.label.toLocaleLowerCase().search(textSearch) !== -1) {
        result.push(item);
      }
    });

    return result;
  }
}

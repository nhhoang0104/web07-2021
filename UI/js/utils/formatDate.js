function formatDate(date) {
  if (!date) return "";

  let tmp = new Date(date);

  let day = `${tmp.getDate() > 9 ? "" : "0"}${tmp.getDate()}`;
  let month = `${tmp.getMonth() > 8 ? "" : "0"}${tmp.getMonth() + 1}`;

  return `${day}\/${month}\/${tmp.getFullYear()}`;
}

function formatDateInput(date) {
  if (!date) return null;
  var tmp = new Date(date);

  var day = ("0" + tmp.getDate()).slice(-2);
  var month = ("0" + (tmp.getMonth() + 1)).slice(-2);

  return tmp.getFullYear() + "-" + month + "-" + day;
}

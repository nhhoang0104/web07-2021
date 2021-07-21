function getDepartment() {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/api/Department",
    method: "GET",
  });
}

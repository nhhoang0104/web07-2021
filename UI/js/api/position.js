function getPosition() {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Positions",
    method: "GET",
  });
}

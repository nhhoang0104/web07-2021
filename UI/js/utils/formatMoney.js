function formatMoney(n, currency = "") {
  return (
    n.toFixed(0).replace(/./g, function (c, i, a) {
      return i > 0 && (a.length - i) % 3 === 0 ? "." + c : c;
    }) + currency
  );
}

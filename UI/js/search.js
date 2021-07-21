function search(textSearch, data) {
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

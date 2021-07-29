import { createApp } from "vue";
import App from "./App.vue";

const app = createApp(App);

import {
  BaseTable,
  BaseTableHead,
  BaseTableBody,
} from "./components/base/table";
import { BaseDropdown, BaseDropdownOption } from "./components/base/dropdown";
import { BaseComboBox, BaseComboBoxOption } from "./components/base/comboBox";

app.component("base-table", BaseTable);
app.component("base-table-head", BaseTableHead);
app.component("base-table-body", BaseTableBody);
app.component("dropdown", BaseDropdown);
app.component("dropdown-option", BaseDropdownOption);
app.component("combo-box", BaseComboBox);
app.component("combo-box-option", BaseComboBoxOption);

app.mount("#app");

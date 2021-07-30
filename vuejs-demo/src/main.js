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
import { BaseButton } from "./components/base/button";
import BaseModal from "./components/base/BaseModal.vue";
import BasePopup from "./components/base/BasePopup.vue";
import BasePagination from "./components/base/BasePagination.vue";

app.component("base-table", BaseTable);
app.component("base-table-head", BaseTableHead);
app.component("base-table-body", BaseTableBody);

app.component("dropdown", BaseDropdown);
app.component("dropdown-option", BaseDropdownOption);

app.component("combo-box", BaseComboBox);
app.component("combo-box-option", BaseComboBoxOption);

app.component("base-button", BaseButton);

app.component("base-modal", BaseModal);
app.component("base-popup", BasePopup);

app.component("base-pagination", BasePagination);

app.mount("#app");

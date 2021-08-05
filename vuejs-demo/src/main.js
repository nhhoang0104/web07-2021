import { createApp } from "vue";
import App from "./App.vue";

const app = createApp(App);

import {
  BaseTable,
  BaseTableHead,
  BaseTableBody,
} from "./components/base/table";
import { BaseDropdown, BaseDropdownOption } from "@/components/base/dropdown";
import { BaseComboBox, BaseComboBoxOption } from "@/components/base/comboBox";
import { BaseButton } from "@/components/base/button";
import BaseModal from "@/components/base/BaseModal.vue";
import BasePopup from "@/components/base/BasePopup.vue";
import BasePagination from "@/components/base/BasePagination.vue";
import BaseInput from "@/components/base/BaseInput.vue";
import BaseToastMessage from "@/components/base/BaseToastMessage.vue";
import BaseDialog from "@/components/base/BaseDialog.vue";
import BaseLoader from "@/components/base/BaseLoader.vue";
import BaseTooltip from "@/components/base/BaseTooltip.vue";

app.component("base-loader", BaseLoader);

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

app.component("base-tooltip", BaseTooltip);
app.component("base-input", BaseInput);

app.component("base-toast-message", BaseToastMessage);

app.component("base-dialog", BaseDialog);

app.component("base-pagination", BasePagination);

app.mount("#app");

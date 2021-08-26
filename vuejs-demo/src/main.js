import { createApp } from "vue";
import router from "./router";
import App from "./App.vue";

const app = createApp(App);

import {
  BaseTable,
  BaseTableHead,
  BaseTableBody,
} from "@/components/base/table";

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

import "devextreme/dist/css/dx.light.css";

app.use(router);

app.component(BaseLoader.name, BaseLoader);

app.component(BaseTable.name, BaseTable);
app.component(BaseTableHead.name, BaseTableHead);
app.component(BaseTableBody.name, BaseTableBody);

app.component(BaseDropdown.name, BaseDropdown);
app.component(BaseDropdownOption.name, BaseDropdownOption);

app.component(BaseComboBox.name, BaseComboBox);
app.component(BaseComboBoxOption.name, BaseComboBoxOption);

app.component(BaseButton.name, BaseButton);

app.component(BaseModal.name, BaseModal);
app.component(BasePopup.name, BasePopup);

app.component(BaseTooltip.name, BaseTooltip);
app.component(BaseInput.name, BaseInput);

app.component(BaseToastMessage.name, BaseToastMessage);

app.component(BaseDialog.name, BaseDialog);

app.component(BasePagination.name, BasePagination);

app.mount("#app");

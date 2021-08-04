import BaseAPI from "@/api/base/BaseAPI.js";

class TodoAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "todos";
  }
}

export default new TodoAPI();

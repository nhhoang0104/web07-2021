import BaseAPI from "@/api/base/BaseAPI.js";

class PositionAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "Positions";
  }
}

export default new PositionAPI();

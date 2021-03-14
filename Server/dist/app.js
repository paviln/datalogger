"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
require("reflect-metadata");
const routing_controllers_1 = require("routing-controllers");
const mongoose_1 = __importDefault(require("mongoose"));
const app = routing_controllers_1.createExpressServer({
    routePrefix: '/api',
    controllers: [__dirname + '/controllers/*.js'],
});
mongoose_1.default.connect('localhost')
    .then(result => {
    app.listen(3000, () => {
        console.log('App listening on port 3000.');
    });
})
    .catch(error => console.log(error));
//# sourceMappingURL=app.js.map
import express from "express";
import * as loggerController from "../controllers/logger.controller";

const router = express.Router();

router.route('/')
    .post(loggerController.create)
    .put(loggerController.update);

router.route('/warnings')
    .post(loggerController.findWarningsInLogs);

export default router;
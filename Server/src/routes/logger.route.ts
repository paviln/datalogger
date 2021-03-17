import express from "express";
import LoggerController from "../controllers/logger.controller";

const router = express.Router();
const loggerController = new LoggerController();

router.route('/')
    .post(loggerController.create);

export default router;
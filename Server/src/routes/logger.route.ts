import express from 'express';
import * as loggerController from '../controllers/logger.controller';

// eslint-disable-next-line new-cap
const router = express.Router();

router.get('/:id', loggerController.getLogger)

router.route('/')
    .get(loggerController.getLoggers)
    .post(loggerController.create)
    .put(loggerController.update);

router.route('/warnings')
    .post(loggerController.findWarningsInLogs);

export default router;

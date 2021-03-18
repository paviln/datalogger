import express from 'express';

import loggerRoutes from './logger.route';
import logRoutes from './log.route';

// eslint-disable-next-line new-cap
const router = express.Router();

router.use('/logger', loggerRoutes);
router.use('/log', logRoutes);

export default router;

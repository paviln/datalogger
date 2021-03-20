import express from 'express';

import loggerRoutes from './logger.route';
import logRoutes from './log.route';
import plantRoutes from './plant.route';

// eslint-disable-next-line new-cap
const router = express.Router();

router.use('/logger', loggerRoutes);
router.use('/log', logRoutes);
router.use('/plant', plantRoutes);

export default router;

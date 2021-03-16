import express from 'express';

import logRoutes from './log.route'

const router = express.Router();

router.use('/log', logRoutes);

export default router;
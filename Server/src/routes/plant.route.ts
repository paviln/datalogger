import express from 'express';
import * as plantController from '../controllers/plant.controller';
import multer from 'multer';

// eslint-disable-next-line new-cap
const router = express.Router();

const upload = multer();
router.route('/')
    .post(upload.single('image'), plantController.create);

export default router;

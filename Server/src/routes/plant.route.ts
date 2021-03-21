import express from 'express';
import {create} from '../controllers/plant.controller';
import multer from 'multer';

// eslint-disable-next-line new-cap
const router = express.Router();

var upload = multer();
router.route('/')
    .post(upload.single("image"), create);

export default router;

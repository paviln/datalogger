import { NextFunction, Request, Response } from 'express';

import Log, { ILog } from '../models/log';
import Logger, { ILogger } from '../models/logger';

export default class LoggerController {
    async create(req: Request, res: Response, next: NextFunction) {
        let newLogger: ILogger = new Logger(req.body);
    
        let exsist: Boolean = await Logger.exists({ _id: newLogger._id });
    
        if (!exsist) {
            newLogger.save((err, log) => {
                if (err) {
                    res.send(err);
                }
                res.status(201).json(log);
            });
        } else {
            res.status(404).send();
        }
    } 
}
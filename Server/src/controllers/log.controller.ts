import { NextFunction, Request, Response } from 'express';

import Log, { ILog } from '../models/log';
import Logger from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction) => {
    let newLog: ILog = new Log(req.body);

    let doesLoggerExsist: Boolean = await Logger.exists({ _id: newLog.loggerId });

    if (doesLoggerExsist) {
        newLog.save((err, log) => {
            if (err) {
                res.send(err);
            }
            res.status(201).json(log);
        });
    } else {
        res.status(404).send();
    }
};

export { create };
import { Request, Response } from 'express';

import Log, { ILog } from '../models/log';

export const create = async (req: Request, res: Response, next:any) => {
    let newLog: ILog = new Log(req.body);

    newLog.save((err, log) => {
        if (err) {
            res.send(err);
        }
        res.json(log);
    });
} 
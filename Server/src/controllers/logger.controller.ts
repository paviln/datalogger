import { NextFunction, Request, Response } from 'express';

import Log, { ILog } from '../models/log';
import Logger, { ILogger } from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    
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
};

const findWarningsInLogs = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    let exsist: Boolean = await Logger.exists({ _id: req.body.loggerId });
    if (exsist) {
        var logs: ILog[] = await getLogsInPeriod(15);
        res.json(logs).send();
    } else {
        res.status(404).send();
    }
};

const getLogsInPeriod = async(period: number): Promise<ILog[]> => {
    var logs: ILog[];
    var start = new Date(new Date().getTime() - (period * 60 * 1000));
    await Log.find( { 'createdAt': { "$gte": start } }, (err, result) => {
        if (err) {
            logs = [];
        } else {
            logs = result;
        }
    });

    return logs;
}

export { create, findWarningsInLogs };
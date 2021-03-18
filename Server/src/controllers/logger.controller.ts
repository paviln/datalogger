import {NextFunction, Request, Response} from 'express';

import Log, {ILog} from '../models/log';
import Logger, {ILogger} from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  const newLogger: ILogger = new Logger(req.body);
  const exsist: Boolean = await Logger.exists({_id: newLogger._id});

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

const update = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  await Logger.findByIdAndUpdate(req.body._id, req.body, {new: true}, (err, doc) => {
    if (err) {
      res.send(err);
    } else {
      res.status(201).json(doc);
    }
  });
};

const findWarningsInLogs = async (req: Request,
    res: Response,
    next: NextFunction): Promise<void> => {
  const logger: ILogger = await Logger.findById(req.body.loggerId);
  if (logger) {
    const logs: ILog[] = await getLogsInPeriod(15);

    res.json(logs).send();
  } else {
    res.status(404).send();
  }
};

const getLogsInPeriod = async (period: number): Promise<ILog[]> => {
  let logs: ILog[];
  const start = new Date(new Date().getTime() - (period * 60 * 1000));
  await Log.find({
    $and: [
      {createdAt: {$gte: start}},
      {temperature: {$gte: 22}},
    ],
  }, function(err: any, result: any) {
    if (err) {
      logs = [];
    } else {
      logs = result;
    }
  });

  return logs;
};

export {create, update, findWarningsInLogs};

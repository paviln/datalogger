import {NextFunction, Request, Response} from 'express';

import Plant, {IPlant, Status} from '../models/plant';
import Logger from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction) => {
  await Logger.exists({_id: req.body.loggerId}, async (err, exists: Boolean) => {
    if (err) {
      res.status(404).send(err);
    } else if (exists) {
      await Plant.exists({
        $and: [
          {loggerId: req.body.loggerId},
          {status: Status.ACTIVE},
        ],
      }, (err: any, exists: Boolean) => {
        if (err) {
          res.status(404).json(err);
        } else if (exists) {
          res.status(404).send();
        } else {
          const plant: any =
          {
            name: req.body.name,
            minimumTemperature: req.body.minimumTemperature,
            soilType: req.body.soilType,
            img: {
              data: req.file.buffer,
              contentType: 'image/png',
            },
            status: req.body.status,
            loggerId: req.body.loggerId,
          };
          Plant.create(plant, (err: any, plant: any) => {
            if (err) {
              res.status(404).send(err);
            }
            res.status(201).json(plant);
          });
        }
      });
    }
  });
};

const getFile = async (req: Request, res: Response, next: NextFunction) => {
  await Plant.findById(req.params.id, (err: any, plant: IPlant) => {
    if (err) {
      res.status(404).send(err);
    } else {
      res.status(200).send(plant.img);
    }
  }).select('img -_id');
};

export {create, getFile};

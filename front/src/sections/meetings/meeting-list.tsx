import React from "react";
import { Link } from "react-router-dom";

import {Login, VideoFile, ContentCopy, Edit as EditIcon} from '@mui/icons-material';
import { Table, Button, Tooltip, TableRow, TableBody, TableHead, TableCell, Typography, TableContainer } from "@mui/material";

import { API_CONSTANTS } from "src/utils/globals/api-constants";
import { timeSuccessAlert } from "src/utils/helpers/alert-helper";

import { MeetingModel } from "src/models/meeting";

export default function MeetingList({meetings, onMeetingClick, isArchive} : {meetings?: MeetingModel[], onMeetingClick: (meeting: MeetingModel) => void, isArchive: boolean}) {
  const getLink = (id: string) => `http://${API_CONSTANTS.clientBaseUrl}/room/${id}`;
  

  const copyLink = (id: string) => {
    const link = getLink(id);
    navigator.clipboard.writeText(link);
    timeSuccessAlert(`Copied ${link}`);
  };


  return meetings != null && meetings?.length > 0 ? (
    <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Title</TableCell>
              <TableCell>Assigned Time</TableCell>
              <TableCell />
            </TableRow>
          </TableHead>
          <TableBody>
            {meetings?.map((m, index) => {
              const assignedDateTime = m.assignedTime ? new Date(m.assignedTime) : null;
            return (
              <TableRow key={index}>
                <TableCell>{m.title}</TableCell>
                <TableCell>{assignedDateTime && assignedDateTime?.toLocaleDateString()} {assignedDateTime && assignedDateTime?.toLocaleTimeString()}</TableCell>
                <TableCell>
                  <Tooltip title="Edit meeting">
                    <Button color="warning" variant="text" onClick={() => onMeetingClick(m)}>
                      <EditIcon />
                    </Button>
                  </Tooltip>
                  <Tooltip title="Copy link">
                    <Button color="secondary" variant="text" onClick={() => copyLink(m.id)}>
                    <ContentCopy/>
                    </Button>
                  </Tooltip>
                  {
                    !isArchive && 
                    <Tooltip title="Join room">
                      <Link to={getLink(m.id)}>
                        <Button color="primary" variant="text"  >
                          <Login />
                        </Button>
                      </Link>
                    </Tooltip>
                  }
                  {
                    isArchive && (m.recordLink != null) &&
                    <Tooltip title="Get record">
                      <a target="_blank" href={`${API_CONSTANTS.staticFilesUrl  }/${  m.recordLink}`} rel="noreferrer">
                        <Button color="primary" variant="text">
                          <VideoFile/>
                        </Button>
                      </a>
                    </Tooltip>
                  }
                </TableCell>
              </TableRow>
              )})}
          </TableBody>
        </Table>
    </TableContainer>
  ) : (
    <Typography>
      There is no records...
    </Typography>
  );
}
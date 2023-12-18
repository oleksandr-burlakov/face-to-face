import React, { useState, useEffect } from "react"

import { Add } from "@mui/icons-material";
import { Box, Tab, Card, Tabs, Stack, Button, Container, Typography } from "@mui/material"

import { MeetingModel } from "src/models/meeting";
import { getMyMeetings } from "src/api/meeting-api";

import MeetingList from "../meeting-list";
import MeetingDialogForm from "./modal/meeting-dialog-form";

function CustomTabPanel(props : CustomTabPanelPropTypes) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ paddingY: 3 }}>
          {children}
        </Box>
      )}
    </div>
  );
}

export type CustomTabPanelPropTypes = {
  children: any,
  index: number,
  value: number,
};

// Keep list of calls. Close all calls on user leave 

export function MeetingView() {
  const [value, setValue] = React.useState(0);
  const [dialogOpen, setDialogOpen] = useState<boolean>(false);
  const [activeMeeting, setActiveMeeting] = useState<MeetingModel | null>();
  const [meetings, setMeetings] = useState<MeetingModel[]>();
      
  const handleChange = (event: any, newValue: number) => {
    setValue(newValue);
  };

  const createNew = () => {
    setDialogOpen(true);
  };

  async function fetchMeetingList() {
    const result = await getMyMeetings();
    if (result.data.succeeded) {
      setMeetings(result.data.result);
    }
  }

  const closeModal = async (reloadList: boolean) => {
    setDialogOpen(false);
    if (reloadList) {
      fetchMeetingList();
    }
    setActiveMeeting(null);
  };

  useEffect(() => {
    fetchMeetingList();
  }, []);

  const editMeeting = (meeting: MeetingModel) => {
    setActiveMeeting(meeting);
    setDialogOpen(true);
  };

  return (
    <Container>
      <Stack mb={5}>
        <Typography variant="h4">Meetings</Typography>
      </Stack>
      <Card>
        <Box padding={4}>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <Stack flexDirection='row'>
              <Tabs sx={{width:'100%'}} value={value} onChange={handleChange} centered textColor="secondary" indicatorColor="secondary">
                <Tab label="Active"/>
                <Tab label="Archive"/>
              </Tabs>
              <Button onClick={createNew} color="secondary" variant="outlined">
                <Add />
              </Button>
              < MeetingDialogForm open={dialogOpen} closeModal={closeModal} meeting={activeMeeting} />
            </Stack>
          </Box>
          <CustomTabPanel value={value} index={0} >
            <MeetingList onMeetingClick={editMeeting} meetings={meetings?.filter(m => !m.isFinished)} isArchive={false} />
          </CustomTabPanel>
          <CustomTabPanel value={value} index={1}>
            <MeetingList onMeetingClick={editMeeting} meetings={meetings?.filter(m => m.isFinished)} isArchive />
          </CustomTabPanel>
        </Box>
      </Card>
    </Container>
  )
}



